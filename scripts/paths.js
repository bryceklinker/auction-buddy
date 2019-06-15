const path = require('path');

const root = path.resolve(__dirname, '..');
module.exports = {
    solution: path.resolve(root, 'Auction.Buddy.sln'),
    function: path.resolve(root, 'src', 'Auction.Buddy.Function'),
    core_tests: path.resolve(root, 'tests', 'Auction.Buddy.Core.Tests'),
    mobile_tests: path.resolve(root, 'tests', 'Auction.Buddy.Mobile.Tests'),
    nunit_console: path.resolve(root, 'packages', 'NUnit.ConsoleRunner.3.10.0', 'tools', 'nunit3-console.exe'),
    acceptance_tests_output: path.resolve(root, 'tests', 'Auction.Buddy.Mobile.Device.Tests', 'bin', 'Release'),
    acceptance_tests_dll: path.resolve(root, 'tests', 'Auction.Buddy.Mobile.Device.Tests', 'bin', 'Release', 'Auction.Buddy.Mobile.Device.Tests.dll')
};