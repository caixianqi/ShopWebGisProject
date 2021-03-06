module.exports = {
  root: true,
  env: {
    node: true,
    es6: true,
    browser: true,
  },
  parser: 'vue-eslint-parser',
  parserOptions: {
    sourceType: 'module',
  },
  extends: ['eslint:recommended', 'plugin:prettier/recommended'],
  rules: {
    'no-debugger': 'off',
    'no-console': 'off',
    //强制使用单引号
    quotes: ['error', 'single'],
    //强制不使用分号结尾
    // 缩进风格
    indent: ['error', 2],
  },
}
