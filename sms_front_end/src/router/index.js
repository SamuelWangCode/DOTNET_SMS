import Vue from 'vue'
import VueRouter from 'vue-router'
import Information from '../views/Information.vue'
import Index from '../views/Index.vue'
import Login from '../views/Login.vue'
import Password from '../views/Password.vue'
import BroadCast from '../views/BroadCast.vue'
import Grade from '../views/Grade.vue'
import Discussion from '../views/Discussion.vue'
import StudentManagement from '../views/StudentManagement.vue'
import TeacherManagement from '../views/TeacherManagement.vue'
import ClassManagement from '../views/ClassManagement.vue'


Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    alias: '/index',
    name: 'Index',
    component: Index
  },
  {
    path: '/information',
    name: 'Information',
    component: Information
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/password',
    name: 'Password',
    component: Password
  },
  {
    path: '/broadCast',
    name: 'BroadCast',
    component: BroadCast
  },
  {
    path: '/grade',
    name: 'Grade',
    component: Grade
  },
  {
    path: '/discussion',
    name: 'Discussion',
    component: Discussion
  },
  {
    path: '/studentManagement',
    name: 'StudentManagement',
    component: StudentManagement
  },
  {
    path: '/teacherManagement',
    name: 'TeacherManagement',
    component: TeacherManagement
  },
  {
    path: '/classManagement',
    name: 'ClassManagement',
    component: ClassManagement
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
