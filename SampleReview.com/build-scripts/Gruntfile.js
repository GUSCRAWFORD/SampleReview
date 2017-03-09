module.exports = function (grunt) {

	// Project configuration.
	grunt.initConfig({
		pkg: grunt.file.readJSON('package.json'),
		watch: {
			scripts: {
				files: ['./app/**/*.js', './ux/**/*.js', './ux/**/*.html', './tests/**/*.js'],
				tasks: ['webpack'],
				options: {
					spawn: false,
				},
			},
			sass: {
				files: ['./ux/**/*.scss'],
				tasks: ['sass'],
				options: {
					spawn: false,
				},
			},
		},
		sass: {
			dist: {
				files: {
					'./css/review.style.css': './ux/review.style.scss'
				}
			},
			options: {
				sourceMap: true
			}
		},
		webpack: {
			review: require('./webpack.config.js')
		}
	});


	require('load-grunt-tasks')(grunt);
	grunt.loadNpmTasks('grunt-webpack');
	grunt.loadNpmTasks('grunt-contrib-watch');

	// Default task(s).
	grunt.registerTask('default', ['sass', 'webpack', 'watch']);
	grunt.registerTask('build', ['sass', 'webpack']);
};