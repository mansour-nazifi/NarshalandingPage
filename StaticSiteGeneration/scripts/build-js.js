const fs = require('fs');
const path = require('path');
const { minify } = require('terser');

const root = path.resolve(__dirname, '..');
const files = [
  'wwwroot/scripts/script.js',
  'wwwroot/scripts/custom-tailoring.js'
];

(async () => {
  for (const rel of files) {
    const input = path.join(root, rel);
    if (!fs.existsSync(input)) {
      console.error(`JS minify skipped: ${rel} not found`);
      continue;
    }
    const output = input.replace(/\.js$/, '.min.js');
    const code = fs.readFileSync(input, 'utf8');
    const result = await minify(code, {
      compress: true,
      mangle: true,
      format: { comments: false }
    });
    if (result.error) throw result.error;
    fs.writeFileSync(output, result.code, 'utf8');
    console.log(`Minified ${path.relative(root, input)} -> ${path.relative(root, output)} (${code.length} -> ${result.code.length})`);
  }
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
