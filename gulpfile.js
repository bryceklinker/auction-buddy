const {series} = require('gulp');
const { unit_tests } = require('./scripts/unit_tests');
const { build_solution } = require('./scripts/build_solution');
const { acceptance_tests } = require('./scripts/acceptance_tests');
const { clean } = require('./scripts/clean');

module.exports = {
    build: build_solution,
    unit_tests,
    acceptance_tests: series(build_solution, acceptance_tests),
    clean,
    default: series(build_solution, unit_tests, acceptance_tests)
};