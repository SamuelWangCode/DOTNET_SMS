<template>
    <Menu :active-name="activeName" :open-names="openNames" class="nav">
        <Submenu name="1">
            <template slot="title">
                <Icon type="ios-analytics" />
                个人信息管理
            </template>
                <router-link :to="{ path: '/information' }"><MenuItem name="1-1">个人信息修改</MenuItem></router-link>
                <router-link :to="{ path: '/password' }"><MenuItem name="1-2">密码修改</MenuItem></router-link>
        </Submenu>
        <Submenu name="2" :disable="adminNot">
            <template slot="title">
                <Icon type="ios-filing" />
                我的班级
            </template>
            <router-link :to="{ path: '/broadCast' }"><MenuItem name="2-1" >班级公告</MenuItem></router-link>
            <router-link :to="{ path: '/grade' }"><MenuItem name="2-2">成绩</MenuItem></router-link>
            <!-- <router-link :to="{ path: '/discussion' }"><MenuItem name="2-3" >班级讨论</MenuItem></router-link> -->
        </Submenu>
        <Submenu name="3" >
            <template slot="title">
                <Icon type="ios-cog" />
                信息管理
            </template>
            <router-link :to="{ path: '/studentManagement' }"><MenuItem name="3-1">学生信息管理</MenuItem></router-link>
            <router-link :to="{ path: '/teacherManagement' }"><MenuItem name="3-2">教师信息管理</MenuItem></router-link>
            <router-link :to="{ path: '/classManagement' }"><MenuItem name="3-3">班级信息管理</MenuItem></router-link>
        </Submenu>
    </Menu>
</template>
<style scoped>
.nav{
    position:fixed;
    height: 100vh;
}
</style>
<script>
    export default {
        name:"navMenu",
        data(){
            return{
                adminNot:true,
                adminOnly:false
            }
        },
        mounted(){
            console.log(this.$store.state.role)
            if (this.$store.state.role == "admin"){
                this.adminNot = false
                this.adminOnly = true
            }
        },
        props:{
            activeName : String,
            openNames : Array
        }
    }
</script>
