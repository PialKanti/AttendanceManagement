import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import { router } from './routes/router'
// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import './assets/main.css'
import "@mdi/font/css/materialdesignicons.css"

const vuetify = createVuetify({
    components,
    directives
})

createApp(App).use(vuetify).use(createPinia()).use(router).mount('#app')
