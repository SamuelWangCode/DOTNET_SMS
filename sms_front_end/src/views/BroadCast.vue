<template>
<div>
<nav-menu :activeName="'2-1'" :openNames="['2']"></nav-menu>
<div class="content">
  <div v-if="adminOnly">
        您没有权限查看
    </div>
  <div v-if="adminNot">
    <Table class="info" :columns="columns" :data="data">
        
    </Table>
  </div>
  <Modal v-model="showModal" title="通知" ><p>{{content}}</p></Modal>
  <Modal v-model="showModal2" title="编辑" @on-ok="submit()" ><Input v-model="value"></Input></Modal>
</div>
</div>
</template>
<style scoped>
.content{
  margin-left:30%;
  width:50%
}
</style>
<script>
import navMenu from '../components/Menu'
export default {
  name: 'BroadCast',
  components:{
    navMenu
  },
  data(){
      var _this = this
      return{
          index:"",
          value:'',
          content:"",
          description:"查看",
          showModal:false,
          showModal2:false,
          adminOnly:false,
          adminNot:true,
          teacherOnly: false,
          columns: [
                    {
                        title: '课程',
                        key: 'course_name'
                    },
                    {
                        title: '公告',
                        key: 'broadCast'
                    },
                    {   
                        title: '动作',
                        key: 'action',
                        render: (h, params) => {
                            return h('div', [
                                h('Button', {
                                    props: {
                                        type: 'primary',
                                        size: 'small',
                                    },
                                    style: {
                                        marginRight: '5px'
                                    },
                                    on: {
                                        click: () => {
                                            this.show(params.index)
                                        }
                                    }
                                }, this.description)
                                ]);
                        }
                    }
                ],
                data: [
                ]
      }
  },
  mounted(){
      if(this.$store.state.role=="admin"){
          this.adminOnly = true
          this.adminNot = false
      }
      if(this.$store.state.role=="teacher"){
          this.teacherOnly = true
          this.description = "编辑"
      }
      let data = {
          Id: this.$store.state.Id
      }
      this.findClassesById(data).then(Response =>{
          if(Response.data.code == 200){
              this.data = Response.data.data
          }
          else{
              this.$Notice.error({
                    title: Response.data.message,
                    desc: ""
                    });
          }
      })
  },
  methods:{
      show (index) {
          this.index = index
           console.log("start")
          if(this.$store.state.role=="student"){
            this.showModal = true
             this.content = this.data[index].broadCast
          }
        else if(this.$store.state.role=="teacher"){
            this.showModal2 = true
        }
      },
      submit(){
          var index = this.index
          let data={
                course_id: this.data[index].course_id,
                broadCast: this.value
            }
            this.changeBroadCast(data).then(Response=>{
                if(Response.data.code==200){
                    this.$Notice.success({
                    title: "编辑成功",
                    desc: ""
                    });
                    this.$router.go(0);
                }else{
                    this.$Notice.error({
                    title: Response.data.message,
                    desc: ""
                    });
                }
            })
      }
  }
}
</script>
