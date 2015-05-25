var gulp = require('gulp');
var bower = require('bower');


gulp.task('bower', function (cb) {
    log('Running bower update');
    var bowerDependencies = require(bower.config.cwd + '/bower.json');
    bower.commands['update'](bowerDependencies).on('end', function (updated) {
        log('Bower updated');
        cb();
    });
    
});

function log(message) {
    console.log(message);
}