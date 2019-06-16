const {series} = require('gulp');
const paths = require('./paths');

const { log_header } = require('./logging_helpers');
const {execute_command} = require("./process_helpers");


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

module.exports = { unit_tests: series(mobile_tests, core_tests) };