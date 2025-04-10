import { createStore } from 'vuex'

let user = null;
try {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
        user = JSON.parse(storedUser);
    }
} catch (error) {
    console.error("Failed to parse user from localStorage:", error);
    localStorage.removeItem("user");
}

const store = createStore({
    state: {
        // 初始化 token 和 user
        token: localStorage.getItem("token") || null,
        user: JSON.parse(localStorage.getItem("user")) || null,
        // 依賴 token 來判斷登入狀態
        isLoggedIn: !!localStorage.getItem("token"),
    },
    mutations: {
        // 設定 token
        setToken(state, token) {
            state.token = token;
        },
        // 設定 user
        setUser(state, user) {
            state.user = user;
        },
        // 登入後，設置登入狀態
        login(state, { token, user }) {
            state.token = token;
            state.user = user;
            state.isLoggedIn = true;

            // 儲存 token 和 user 至 localStorage
            localStorage.setItem("token", token);
            localStorage.setItem("user", JSON.stringify(user));
        },
        // 登出時，清除登入狀態
        logout(state) {
            state.token = null;
            state.user = null;
            state.isLoggedIn = false;

            // 清除 localStorage
            localStorage.removeItem("token");
            localStorage.removeItem("user");
        },
    },
    actions: {
        // 用於登出操作
        logout({ commit }) {
            commit('logout');
        },
    },
    getters: {
        // 獲取登入狀態
        isLoggedIn(state) {
            return state.isLoggedIn;
        },
        // 獲取 user 資料
        getUser(state) {
            return state.user;
        },
        // 獲取 token
        getToken(state) {
            return state.token;
        },
    },
})

export default store