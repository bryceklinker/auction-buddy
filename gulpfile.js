const path = require('path');
const os = require('os');
const {series, parallel} = require('gulp');
const {spawn} = require('child_process');

const paths = {
    solution: path.resolve(__dirname, 'Auction.Buddy.sln'),
    function: path.resolve(__dirname, 'src', 'Auction.Buddy.Function'),
    core_tests: path.resolve(__dirname, 'tests', 'Auction.Buddy.Core.Tests'),
    mobile_tests: path.resolve(__dirname, 'tests', 'Auction.Buddy.Mobile.Tests'),
    nunit_console: path.resolve(__dirname, 'packages', 'NUnit.ConsoleRunner.3.10.0', 'tools', 'nunit3-console.exe'),
    acceptance_tests_output: path.resolve(__dirname, 'tests', 'Auction.Buddy.Mobile.Device.Tests', 'bin', 'Release'),
    acceptance_tests_dll: path.resolve(__dirname, 'tests', 'Auction.Buddy.Mobile.Device.Tests', 'bin', 'Release', 'Auction.Buddy.Mobile.Device.Tests.dll')
};

function mobile_tests(cb) {
    log_header('RUNNING MOBILE TESTS');
    execute_command('dotnet', ['test'], {cwd: paths.mobile_tests})
        .then(() => cb())
        .catch(cb);
}

function core_tests(cb) {
    log_header('RUNNING CORE TESTS');
    execute_command('dotnet', ['test'], {cwd: paths.core_tests})
        .then(() => cb())
        .catch(cb);
}

function build_solution(cb) {
    log_header('BUILDING SOLUTION');
    execute_command('msbuild', [paths.solution, '/t:Rebuild', '/p:Configuration=Release'])
        .then(() => cb())
        .catch(cb);
}

function start_function() {
    log_header('STARTING FUNCTION');
    return start_process('func', ['start', '--build'], {cwd: paths.function});
}

function acceptance_tests(cb) {
    let process;
    start_function()
        .then(p => process = p)
        .then(when_function_ready)
        .then(execute_acceptance_tests)
        .finally(stop_function)
        .then(() => cb())
        .catch(cb);
}

function when_function_ready(process) {
    log_header('WAITING FOR FUNCTION TO START');
    return new Promise((resolve, reject) => {
        process.stdout.on('data', data => {
            if (`${data}`.includes('Now listening')) {
                resolve();
            }
        })
    });
}

function execute_acceptance_tests() {
    log_header('RUNNING ACCEPTANCE TESTS');
    return os.platform === 'win32'
        ? execute_command(paths.nunit_console, [paths.acceptance_tests_dll], {cwd: paths.acceptance_tests_output})
        : execute_command('mono', [paths.nunit_console, paths.acceptance_tests_dll], {cwd: paths.acceptance_tests_output});
}

function stop_function() {
    log_header('STOPPING FUNCTION');
    return execute_command('pkill', ['-f', 'func'], {});
}

function execute_command(command, args, options) {
    return new Promise((resolve, reject) => {
        const process = spawn(command, args, options);
        process.stdout.on('data', (data) => {
            console.log(`${command} OUTPUT: ${data}`);
        });

        process.stderr.on('error', (data) => {
            console.error(`${command} ERROR: ${data}`);
        });

        process.on('close', (exitCode) => {
            console.log(`${command} exited with code: ${exitCode}`);
            if (exitCode !== 0) {
                reject(exitCode);
            } else {
                resolve();
            }
        });
    });
}

function start_process(command, args, options) {
    return new Promise((resolve, reject) => {
        const process = spawn(command, args, options);
        process.stdout.on('data', (data) => {
            console.log(`${command} OUTPUT: ${data}`);
        });

        process.stderr.on('error', (data) => {
            console.error(`${command} ERROR: ${data}`);
        });

        process.on('close', (exitCode) => {
            console.log(`${command} exited with code: ${exitCode}`);
        });
        resolve(process);
    });
}

function log_header(message) {
    console.log(`*** ${message} ***`)
}

exports.build = series(build_solution);
exports.unit_tests = series(exports.build, parallel(mobile_tests, core_tests));
exports.acceptance_tests = series(exports.build, acceptance_tests);
exports.default = series(
    exports.unit_tests,
    exports.acceptance_tests
);