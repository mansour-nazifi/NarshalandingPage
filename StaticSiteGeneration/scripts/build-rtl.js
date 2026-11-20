// Converts the compiled LTR stylesheet into an RTL stylesheet using rtlcss, then minifies.
// Usage: node scripts/build-rtl.js

const fs = require('fs');
const path = require('path');
const postcss = require('postcss');
const rtlcss = require('rtlcss');
const cssnano = require('cssnano');

const root = path.resolve(__dirname, '..');
const input = path.join(root, 'wwwroot', 'styles', 'site.css');
const output = path.join(root, 'wwwroot', 'styles', 'site.rtl.css');

if (!fs.existsSync(input)) {
    console.error(`RTL build skipped: ${input} not found (run "npm run build:css" first).`);
    process.exit(0);
}

const css = fs.readFileSync(input, 'utf8');

postcss([rtlcss, cssnano])
    .process(css, { from: input, to: output })
    .then((result) => {
        fs.writeFileSync(output, result.css, 'utf8');
        console.log(`RTL stylesheet written: ${path.relative(root, output)}`);
    })
    .catch((error) => {
        console.error(error);
        process.exit(1);
    });
