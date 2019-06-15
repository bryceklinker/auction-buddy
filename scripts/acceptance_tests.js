const os = require('os');

const {start_process, execute_command } = require('./process_helpers');
const { log_header } = require('./logging_helpers');
const paths = require('./paths');

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

function start_function() {
    log_header('STARTING FUNCTION');
    return start_process('func', ['start', '--build'], {cwd: paths.function});
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

module.exports = { acceptance_tests };