import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:5000/api/",
});

// Request interceptor to add access token to requests
api.interceptors.request.use(
  (config) => {
    const tokens = JSON.parse(localStorage.getItem("tokens") || "{}");
    if (tokens.accessToken) {
      config.headers.Authorization = `Bearer ${tokens.accessToken}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor to handle token refresh
api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    const originalRequest = error.config;

    // If the error is 401 and we haven't already tried to refresh
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const tokens = JSON.parse(localStorage.getItem("tokens") || "{}");

        if (tokens.refreshToken) {
          // Call refresh token endpoint
          const response = await axios.post(
            `${import.meta.env.VITE_API_URL}Auth/refresh-token`,
            {
              refreshToken: tokens.refreshToken,
            }
          );
          console.log("token refreshed__________________");
          const newTokens = response.data;

          // Store new tokens
          localStorage.setItem(
            "tokens",
            JSON.stringify({
              accessToken: newTokens.accessToken,
              refreshToken: newTokens.refreshToken,
              accessTokenExpiresAt: newTokens.accessTokenExpiresAt,
              refreshTokenExpiresAt: newTokens.refreshTokenExpiresAt,
            })
          );

          // Update the original request with new token
          originalRequest.headers.Authorization = `Bearer ${newTokens.accessToken}`;

          // Retry the original request
          return api(originalRequest);
        }
      } catch (refreshError) {
        // If refresh fails, clear tokens and redirect to login
        localStorage.removeItem("tokens");
        localStorage.removeItem("user");
        window.location.href = "/login";
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export default api;
