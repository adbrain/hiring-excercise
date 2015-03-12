var	gulp = require("gulp"),
	less = require("gulp-less"),
	sass = require("gulp-sass");

var options = {
	sass: { style: "nested", indentedSyntax: true },
	scss: { style: "nested" }
};

function compileSass (pathToSource, extension) {
	gulp.src(pathToSource + "/style." + extension)
		.pipe(sass(options[extension]))
		.pipe(gulp.dest(pathToSource));

	console.log("CSS (from sass) successfully compiled!");
}

function compileLess (pathToSource) {
	gulp.src(pathToSource + "/style.less")
		.pipe(less())
		.pipe(gulp.dest(pathToSource));

	console.log("CSS (from less) successfully compiled!");
}

gulp.task("less", function () { compileLess("css"); });
gulp.task("sass", function () { compileSass("css", "sass"); });
gulp.task("scss", function () { compileSass("css", "scss"); });

gulp.task("default", function () {
	gulp.watch("css/*.less", ["less"]);
	gulp.watch("css/*.sass", ["sass"]);
	gulp.watch("css/*.scss", ["scss"]);
});