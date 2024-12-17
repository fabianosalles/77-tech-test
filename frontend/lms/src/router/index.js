import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import BooksView from '@/views/BooksView.vue'

const routes = [  
  { path: '/', name: 'books', component: BooksView },
  { path: '/about', name: 'about',
    component: function () {
      return import( '../views/AboutView.vue')
    }
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
