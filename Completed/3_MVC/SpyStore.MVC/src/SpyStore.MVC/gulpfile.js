/// <binding BeforeBuild='clean, min' Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/"
};

paths.valJs = paths.webroot + "js/validations/**/*.js";
paths.minValJs = paths.webroot + "js/validations/**/*.min.js";
paths.concatValJsDest = paths.webroot + "js/validations/vals.min.js";
paths.js = paths.webroot + "js/site/**/*.js";
paths.minJs = paths.webroot + "js/site/**/*.min.js";
//paths.css = paths.webroot + "css/**/*.css";
//paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site/site.min.js";
//paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
    rimraf(paths.concatValJsDest, cb);
});

//gulp.task("clean:css", function (cb) {
//    rimraf(paths.concatCssDest, cb);
//});

//gulp.task("clean", ["clean:js", "clean:css"]);
gulp.task("clean", ["clean:js"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});
gulp.task("min:valJs", function () {
    return gulp.src([paths.valJs, "!" + paths.minValJs], { base: "." })
        .pipe(concat(paths.concatValJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

//gulp.task("min:css", function () {
//    return gulp.src([paths.css, "!" + paths.minCss])
//        .pipe(concat(paths.concatCssDest))
//        .pipe(cssmin())
//        .pipe(gulp.dest("."));
//});

//gulp.task("min", ["min:js", "min:css"]);
gulp.task("min", ["min:js", "min:valJs"]);
