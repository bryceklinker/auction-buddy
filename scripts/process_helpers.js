const {spawn} = require('child_process');

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

module.exports = { start_process, execute_command };