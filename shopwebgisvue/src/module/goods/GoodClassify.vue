<template>
  <el-container>
    <el-header>
      <table-header
        ref="tableheader"
        @doQuery="doQuery"
        @showAddDialog="showDialogVisible"
      ></table-header>
    </el-header>
    <el-main>
      <el-row v-if="!showEmpty" v-loading="tableLoading">
        <div>
          <el-table
            :data="tableData"
            :border="true"
            stripe
            row-key="id"
            size="medium "
            :header-cell-style="{ color: '#fff', backgroundColor: '#409EFF' }"
            style="
              border-radius: 4px;
              box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12),
                0 0 6px rgba(0, 0, 0, 0.04);
            "
          >
            <el-table-column
              prop="name"
              label="商品分类"
              width="180"
              header-align="center"
              align="center"
            >
            </el-table-column>
            <el-table-column
              prop="code"
              label="字典编码"
              width="180"
              header-align="center"
              align="center"
            >
            </el-table-column>
            <el-table-column
              prop="rank"
              label="排序"
              header-align="center"
              align="center"
              width="100"
            >
            </el-table-column>
            <el-table-column
              prop="description"
              label="描述"
              header-align="center"
              align="center"
              width="720"
            >
            </el-table-column>
            <el-table-column label="操作" header-align="center" align="center">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="primary"
                  @click="showDataDictionaryItem(scope.row.id)"
                  >字典项明细<i class="el-icon-add el-icon--right"
                /></el-button>
                <el-button
                  size="mini"
                  type="primary"
                  @click="handleEdit(scope.row)"
                  >编辑<i class="el-icon-edit el-icon--right"
                /></el-button>
                <el-button
                  size="mini"
                  type="danger"
                  @click="handleDelete(scope.row)"
                  >禁用<i class="el-icon-delete el-icon--right" />
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
        <div class="pagination">
          <el-pagination
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page.sync="currentPage"
            :page-sizes="[10, 20, 50]"
            :page-size="currentSize"
            layout="prev, jumper, next,sizes "
            :total="total"
          >
          </el-pagination>
        </div>
      </el-row>
      <el-row v-else></el-row>
    </el-main>
    <el-dialog
      title="新增字典"
      :visible.sync="dialogVisible"
      width="30%"
      v-loading="loading"
      @close="dialogClose"
    >
      <el-form>
        <el-form
          ref="form"
          :model="form"
          label-width="100px"
          label-suffix=":"
          :rules="rules"
          label-position="left"
        >
          <el-row>
            <el-col :span="11">
              <el-form-item label="字典名称" prop="name" required>
                <el-input v-model="form.name"></el-input
              ></el-form-item>
            </el-col>
            <el-col :span="11" :offset="2">
              <el-form-item label="字典编码" prop="code" required>
                <el-input v-model="form.code"></el-input
              ></el-form-item> </el-col
          ></el-row>
          <el-form-item label="排序" prop="rank" required>
            <el-row>
              <el-col :span="3">
                <el-input v-model="form.rank"></el-input>
              </el-col>
            </el-row>
          </el-form-item>
          <el-form-item label="字典描述" prop="description" required>
            <el-input type="textarea" v-model="form.description"></el-input
          ></el-form-item>
          <el-form-item class="btn">
            <el-button type="primary" @click="onSubmit('form')">保存</el-button>
            <el-button type="primary" @click="cancel">取消</el-button>
          </el-form-item>
        </el-form>
      </el-form>
    </el-dialog>
  </el-container>
</template>
<script>
import DataDictionaryItem from './DataDictionaryItem'
export default {
  data() {
    return {
      loading: false,
      tableLoading: false,
      showEmpty: false,
      tableData: [],
      currentPage: 1,
      currentSize: 20,
      total: 0,
      dialogVisible: false,
      form: {
        id: 0,
        name: '',
        code: '',
        rank: 0,
        url: '',
        parentId: 0,
        iconClass: '',
      },
      rules: {
        name: [{ required: true, message: '请填写字典名称!', trigger: 'blur' }],
        code: [{ required: true, message: '请填写字典编码!', trigger: 'blur' }],
        rank: [
          { required: true, message: '请填写字典顺序!', trigger: 'blur' },
          {
            type: 'number',
            message: '请输入整数',
            trigger: 'blur',
          },
        ],
        description: [
          { required: true, message: '请填写字典描述!', trigger: 'blur' },
        ],
      },
    }
  },
  components: { DataDictionaryItem },
  mounted() {},
  methods: {
    doQuery() {},
    handleSizeChange(val) {
      this.currentSize = val
      this.doQuery()
    },
    handleCurrentChange(val) {
      this.currentPage = val
      this.doQuery()
    },
    handleEdit(row) {
      console.log(row)
    },
    handleDelete(row) {
      console.log(row)
    },
    showDataDictionaryItem(id) {
      console.log(id)
    },
    showDialogVisible() {
      this.dialogVisible = true
    },
    dialogClose() {
      this.$refs.form.resetFields()
      this.dialogVisible = false
    },
    cancel() {
      this.$refs.form.resetFields()
      this.dialogVisible = false
    },
    onSubmit(formName) {
      console.log(formName)
    },
  },
}
</script>

<style lang="less" scoped>
.pagination {
  display: flex;
  flex-direction: row-reverse;
}
.btn {
  display: flex;
  flex-direction: row-reverse;
}

body {
  font-family: 'Helvetica Neue', Helvetica, 'PingFang SC', 'Hiragino Sans GB',
    'Microsoft YaHei', '微软雅黑', Arial, sans-serif;
}
</style>
