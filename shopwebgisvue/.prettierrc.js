module.exports = {
  semi: false, // 在每个语句的末尾是否使用分号，默认为true
  trailingComma: 'none', // 是否使用尾逗号，有三个可选值"<none|es5|all>"
  singleQuote: true, // 字符串是否使用单引号，默认为false，使用双引号
  printWidth: 220, // 一行的字符数，如果超过会进行换行，默认为80
  tabWidth: 2, // 一个tab代表几个空格数
  useTabs: true, // 启用tab缩进
  bracketSpacing: true, // 对象大括号直接是否有空格，默认为true，效果：{ foo: bar }
  arrowParens: 'avoid', // always 箭头功能括号总是有括号
  jsxSingleQuote: false, //在JSX中使用单引号而不是双引号。
  jsxBracketSameLine: false, //将>多行JSX元素的放在最后一行的末尾，而不是一个人放在下一行（不适用于自闭元素）
  vueIndentScriptAndStyle: false, //Vue文件脚本和样式标签缩进
  endOfLine: 'auto',
}
