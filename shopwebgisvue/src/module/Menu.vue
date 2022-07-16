<template>
  <el-container style="height: 100%">
    <el-container>
      <el-header>
        <el-row>
          <el-col :span="3"
            ><el-input placeholder="菜单名:" v-model="queryStr" clearable>
            </el-input
          ></el-col>
          <el-col :span="2">
            <el-button type="primary" icon="el-icon-search" @click="doQuery"
              >查询</el-button
            >
          </el-col>
          <el-col :span="1">
            <el-button
              type="primary"
              icon="el-icon-plus"
              @click="showDialogVisible"
              >新增</el-button
            >
          </el-col>
        </el-row></el-header
      >
      <el-main>
        <el-row v-if="!showEmpty" v-loading="tableLoading">
          <div>
            <el-table
              :data="tableData"
              :border="true"
              stripe
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
                label="菜单名"
                width="180"
                header-align="center"
                align="center"
              >
              </el-table-column>
              <el-table-column
                prop="url"
                label="菜单地址"
                width="180"
                header-align="center"
                align="center"
              >
              </el-table-column>
              <el-table-column
                prop="sort"
                label="排序"
                header-align="center"
                align="center"
                width="80"
              >
              </el-table-column>
              <el-table-column
                label="图标"
                header-align="center"
                align="center"
                width="80"
              >
                <template slot-scope="scope">
                  <i :class="scope.row.iconClass"></i>
                </template>
              </el-table-column>
              <el-table-column
                label="操作"
                header-align="center"
                align="center"
              >
                <template slot-scope="scope">
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
        <el-row v-else><el-empty description="暂无数据"></el-empty></el-row>
        <el-dialog
          title="菜单"
          :visible.sync="dialogVisible"
          width="30%"
          v-loading="loading"
          @close="dialogClose()"
        >
          <div>
            <el-form
              ref="form"
              :model="form"
              label-width="100px"
              label-suffix=":"
              :rules="rules"
              label-position="left"
            >
              <el-form-item label="菜单名称" prop="name" required>
                <el-input v-model="form.name"></el-input>
              </el-form-item>
              <el-form-item label="父菜单" prop="parentId" required>
                <el-row
                  ><el-col :span="24">
                    <el-dropdown
                      trigger="click"
                      placement="bottom-start"
                      style="width: 100%"
                    >
                      <el-input style="width: 100%" v-model="menuName">
                      </el-input>
                      <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item>
                          <el-tree
                            :data="data"
                            :props="defaultProps"
                            accordion
                            node-key="id"
                            @check-change="getMenuCurrentKey(data)"
                            ref="treeMenuRef"
                            :check-on-click-node="true"
                            show-checkbox
                            :check-strictly="true"
                          ></el-tree
                        ></el-dropdown-item>
                      </el-dropdown-menu> </el-dropdown></el-col
                ></el-row>
              </el-form-item>
              <el-form-item label="菜单路由" prop="url" required>
                <el-input v-model="form.url"></el-input
              ></el-form-item>
              <el-form-item label="菜单图标">
                <el-input v-model="form.iconClass"></el-input
              ></el-form-item>
              <el-form-item label="排序" prop="sort" required>
                <el-row
                  ><el-col :span="3">
                    <el-input-number
                      v-model="form.sort"
                      :min="1"
                      label="菜单排序"
                      controls-position="right"
                      size="small"
                    ></el-input-number></el-col
                ></el-row>
              </el-form-item>
              <el-form-item class="btn">
                <el-button type="primary" @click="onSubmit('form')"
                  >创建</el-button
                >
                <el-button @click="cancel">清空</el-button>
              </el-form-item>
            </el-form>
          </div>
        </el-dialog></el-main
      >
    </el-container>
  </el-container>
</template>
<script>
export default {
  data() {
    return {
      tableData: [],
      form: {
        name: '',
        sort: 1,
        url: '',
        parentId: 0,
        iconClass: '',
      },
      dialogVisible: false,
      rules: {
        name: [{ required: true, message: '请填写菜单名称', trigger: 'blur' }],
        sort: [{ required: true, message: '请填写菜单顺序', trigger: 'blur' }],
        url: [{ required: true, message: '请填写路由', trigger: 'blur' }],
      },
      loading: false,
      tableLoading: false,
      data: [],
      defaultProps: {
        children: 'children',
        label: 'name',
      },
      treeMenuListUrl: '/Menu/GetTreeList',
      MenuListUrl: '/Menu/GetMenuList',
      menuName: '',
      parentIdList: [],
      queryStr: '', // 搜索条件
      currentPage: 1,
      currentSize: 20,
      total: 0,
      showEmpty: false,
    }
  },
  mounted() {
    this.doQuery()
    this.getTreeMenuList()
  },
  methods: {
    onSubmit(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          if (this.parentIdList.length > 1) {
            this.$message.error('只能选择单个菜单节点!')
            return
          }
          this.form.parentId = this.parentIdList[0]
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
    getTreeMenuList() {
      this.loading = true
      this.axios
        .get(this.treeMenuListUrl)
        .then((res) => {
          this.data = res
        })
        .catch((error) => {
          this.$message.error(error)
        })
        .finally(() => {
          this.loading = false
        })
    },
    // 获取选中菜单的Id以及Name
    getMenuCurrentKey() {
      this.menuName = ''
      // const keys = this.$refs.treeMenuRef.getCheckedKeys()

      const keys = this.$refs.treeMenuRef.getCheckedKeys(false)
      const nodes = this.$refs.treeMenuRef.getCheckedNodes()
      nodes.forEach((node) => {
        const menu = node.name
        this.menuName += menu + '/'
      })
      this.parentIdList = keys
    },
    // 弹出框回调
    dialogClose() {
      this.$refs.form.resetFields()
      this.menuName = ''
    },
    // 编辑菜单
    handleEdit(row) {
      console.log(row)
    },
    handleDelete(row) {
      this.tableLoading = true
      this.axios
        .delete('/Menu/DisableMenu?Id=' + row.id)
        .then(() => {
          this.$message.success('禁用成功!')
          this.doQuery()
        })
        .catch((error) => {
          this.$message.error(error)
        })
        .finally(() => {
          this.tableLoading = false
        })
    },
    // 搜索框查询
    doQuery() {
      this.tableLoading = true
      const param = {
        query: this.queryStr,
        pageSize: this.currentSize,
        pageIndex: this.currentPage,
      }
      this.$get(this.MenuListUrl, param)
        .then((res) => {
          this.tableData = res.datas
          this.total = res.totalCount
          if (this.tableData.length <= 0) {
            this.showEmpty = true
          }
        })
        .catch((error) => {
          this.$message.error(error)
        })
        .finally(() => {
          this.tableLoading = false
        })
    },
    handleSizeChange(val) {
      this.currentSize = val
      this.doQuery()
    },
    handleCurrentChange(val) {
      this.currentPage = val
      this.doQuery()
    },
  },
}
</script>
<style lang="less" scoped>
.contatiner {
  display: flex;
  flex-direction: column;
  height: 100%;
}

.main {
  width: 60%;
}
.btn {
  display: flex;
  flex-direction: row-reverse;
}
.el-dropdown-link {
  cursor: pointer;
  color: #409eff;
}
.el-main {
  padding-top: 0;
}
.pagination {
  display: flex;
  flex-direction: row-reverse;
}
</style>
