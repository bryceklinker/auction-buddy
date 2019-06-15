const { log_header } = require('./logging_helpers');
const { execute_command } = require('./process_helpers');
const { solution } = require('./paths');

function build_solution(cb) {
    log_header('BUILDING SOLUTION');
    execute_command('msbuild', [solution, '/t:Rebuild', '/p:Configuration=Release'])
        .then(() => cb())
        .catch(cb);
}

module.exports = { build_solution };