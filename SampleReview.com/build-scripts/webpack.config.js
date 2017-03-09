var path = require('path');
module.exports = {
	entry: './ux/ui.review.module.js',
	devtool: 'source-map',
	output: {
		filename: 'ui.review.js',
		path: path.resolve(__dirname, '../js'),
		devtoolLineToLine: { test: './**' }
	},
	module: {
		rules: [{
			test: /\.scss$/,
			use: [{
				loader: "style-loader"
			}, {
				loader: "css-loader"
			}, {
				loader: "sass-loader",
				options: {
					includePaths: []
				}
			}]
		},
        {
        	test: /\.html$/i,
        	use: [{ loader: 'html-loader', query: { minimized: true } }]
        }]
	}
};