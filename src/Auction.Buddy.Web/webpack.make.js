const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = function make(env) {
    const isProd = isProduction(env);
    return {
        devtool: 'source-map',
        entry: {
            main: path.join(__dirname, 'client-app', 'index.tsx')
        },
        output: {
            path: path.join(__dirname, 'client-app', 'dist'),
            filename: isProd ? '[name].[hash].js' : '[name].js',
            sourceMapFilename: '[file].map',
        },
        resolve: {
            extensions: ['.tsx', '.ts', '.jsx', '.js', '.css', '.scss', '.json']
        },
        module: {
            rules: [
                {
                    test: /\.(ts|tsx|js|jsx)$/,
                    exclude: /node_modules/,
                    use: ['babel-loader']
                },
                {
                    test: /\.scss$/,
                    use: ['style-loader', 'css-loader', 'sass-loader']
                },
                {
                    test: /\.css$/,
                    use: ['style-loader', 'css-loader']
                }
            ]
        },
        plugins: [
            new HtmlWebpackPlugin({
                template: path.join(__dirname, 'client-app', 'index.html'),
                filename: 'index.html'
            })
        ],
        devServer: {
            historyApiFallback: true,
        }
    };
};

function isProduction(env) {
    return env === 'production';
}