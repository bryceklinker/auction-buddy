const { log_header } = require('./logging_helpers');
const { execute_command } = require('./process_helpers');
const { solution } = require('./paths');

function clean(cb) {
    log_header('BUILDING SOLUTION');
    execute_command('msbuild', [solution, '/t:Clean'])
        .then(() => cb())
        .catch(cb);
}

module.exports = { clean };