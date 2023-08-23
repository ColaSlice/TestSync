module.exports = {
  content: [
    // these are relative to package.json
    './UI/Scripts/**/*.js',
    './Views/**/*.cshtml'
  ],
  theme: {
    extend: {
    },
  },
    plugins: [
        require('@tailwindcss/typography')
    ],
}
