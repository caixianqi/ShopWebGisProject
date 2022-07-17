<template>
  <el-row>
    <el-col :span="3"
      ><el-input placeholder="菜单名:" v-model="queryStr" clearable> </el-input
    ></el-col>
    <el-col :span="2">
      <el-button type="primary" icon="el-icon-search" @click="doQuery"
        >查询</el-button
      >
    </el-col>
    <el-col :span="1">
      <el-button type="primary" icon="el-icon-plus" @click="showDialogVisible"
        >新增</el-button
      >
    </el-col>
  </el-row>
</template>
<script>
export default {
  name: 'TableHeader',
  data() {
    return {
      queryStr: '',
    }
  },
  methods: {
    doQuery() {
      this.$emit('doQuery')
    },
    showDialogVisible() {
      this.$emit('showAddDialog')
    },
  },
}
</script>

<style lang="less" scoped></style>
