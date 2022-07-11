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
    <el-row>
      <div>
        <el-table :data="tableData" stripe style="width: 100%">
          <el-table-column prop="date" label="日期" width="180">
          </el-table-column>
          <el-table-column prop="name" label="姓名" width="180">
          </el-table-column>
          <el-table-column prop="address" label="地址"> </el-table-column>
        </el-table>
      </div>
    </el-row>
    <el-dialog
      title="菜单"
      :visible.sync="dialogVisible"
      width="30%"
      v-loading="loading"
    >
      <div>
        <el-form
          ref="form"
          :model="form"
          label-width="100px"
          label-suffix="："
          :rules="rules"
        >
          <el-form-item label="菜单名称" prop="name" required>
            <el-input v-model="form.name"></el-input>
          </el-form-item>
          <el-form-item label="父菜单" prop="parentId"> </el-form-item>
          <el-form-item label="菜单路由" prop="url" required>
            <el-input v-model="form.url"></el-input
          ></el-form-item>
          <el-form-item label="排序" prop="sort" required>
            <el-input-number
              v-model="form.sort"
              :min="1"
              label="菜单排序"
            ></el-input-number>
          </el-form-item>
          <el-form-item class="btn">
            <el-button type="primary" @click="onSubmit('form')">创建</el-button>
            <el-button @click="cancel">清空</el-button>
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
        parentId: 0,
      },
      dialogVisible: false,
      querystr: '',
      rules: {
        name: [{ required: true, message: '请填写菜单名称', trigger: 'blur' }],
        sort: [{ required: true, message: '请填写菜单顺序', trigger: 'blur' }],
        url: [{ required: true, message: '请填写路由', trigger: 'blur' }],
      },
      loading: false,
    }
  },
  methods: {
    onSubmit(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.loading = true
          this.axios
            .post('/Menu/AddMenu', this.form)
            .then(() => {
              this.$refs[formName].resetFields()
              this.$message.success('新增成功!')
              this.dialogVisible = false
            })
            .catch((error) => {
              this.$message.error(error)
            })
            .finally(() => {
              this.loading = false
            })
        } else {
          this.$message.error(this.$t('form_validate'))
          return false
        }
      })
    },
    showDialogVisible() {
      this.dialogVisible = true
    },
    search() {},
    cancel() {
      this.$refs.form.resetFields()
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
