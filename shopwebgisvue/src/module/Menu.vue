<template>
  <div class="contatiner">
    <el-row>
      <el-col :span="3"
        ><el-input placeholder="菜单名" v-model="querystr" clearable> </el-input
      ></el-col>
      <el-col :span="3"
        ><el-button-group>
          <el-button type="primary" icon="el-icon-search">查询</el-button>
          <el-button
            type="primary"
            icon="el-icon-plus"
            @click="showDialogVisible"
            >新增</el-button
          ></el-button-group
        >
      </el-col>
    </el-row>
    <div>
      <el-table :data="tableData" stripe style="width: 100%">
        <el-table-column prop="date" label="日期" width="180">
        </el-table-column>
        <el-table-column prop="name" label="姓名" width="180">
        </el-table-column>
        <el-table-column prop="address" label="地址"> </el-table-column>
      </el-table>
    </div>
    <el-dialog title="菜单" :visible.sync="dialogVisible" width="30%">
      <div>
        <el-form ref="form" :model="form" label-width="100px" label-suffix="：">
          <el-form-item label="菜单名称">
            <el-input v-model="form.name"></el-input>
          </el-form-item>
          <el-form-item label="父菜单"> </el-form-item>
          <el-form-item label="菜单路由">
            <el-input v-model="form.url"></el-input
          ></el-form-item>
          <el-form-item label="排序">
            <el-input-number
              v-model="form.sort"
              :min="1"
              label="菜单排序"
            ></el-input-number>
          </el-form-item>
          <el-form-item class="btn">
            <el-button type="primary" @click="onSubmit">创建</el-button>
            <el-button @click="cancel">取消</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-dialog>
  </div>
</template>
<script>
export default {
  data() {
    return {
      tableData: [
        {
          date: '2016-05-02',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄',
        },
        {
          date: '2016-05-04',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1517 弄',
        },
        {
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄',
        },
        {
          date: '2016-05-03',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1516 弄',
        },
      ],
      form: {
        name: '',
        sort: 1,
        url: '',
      },
      dialogVisible: false,
      querystr: '',
    }
  },
  methods: {
    onSubmit() {
      console.log('submit!')
    },
    showDialogVisible() {
      this.dialogVisible = true
    },
    search() {},
    cancel() {
      this.dialogVisible = false
    },
  },
}
</script>
<style lang="less" scoped>
.contatiner {
  display: flex;
  flex-direction: column;
  height: 100%;
  .top {
    display: flex;
    flex-direction: row;
  }
}

.main {
  width: 60%;
}
.btn {
  display: flex;
  flex-direction: row-reverse;
}
</style>
