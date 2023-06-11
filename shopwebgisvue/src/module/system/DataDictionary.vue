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
              label="字典名称"
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
              prop="description"
              label="描述"
              header-align="center"
              align="center"
              width="80"
            >
            </el-table-column>
            <el-table-column
              prop="rank"
              label="排序"
              header-align="center"
              align="center"
              width="80"
            >
            </el-table-column>
            <el-table-column label="操作" header-align="center" align="center">
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
      <el-row v-else></el-row>
    </el-main>
  </el-container>
</template>
<script>
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
    }
  },
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
  },
}
</script>

<style lang="less" scoped>
.pagination {
  display: flex;
  flex-direction: row-reverse;
}
</style>
