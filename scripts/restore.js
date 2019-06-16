
const { solution } = require('./paths');
const { execute_command } = require('./process_helpers');

function restore(cb) {
    execute_command('dotnet', ['restore'], { cwd: solution })
        .then(() => cb())
        .catch(cb);
}

module.exports = { restore };