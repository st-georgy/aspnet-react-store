import axios, { AxiosResponse } from 'axios';

interface LoginData {
  email: string;
  password: string;
}

interface RegisterData {
  email: string;
  username: string;
  password: string;
}

export const login = async (loginData: LoginData): Promise<boolean> => {
  return axios
    .post('/api/auth/login', loginData)
    .then((response: AxiosResponse<boolean>) => {
      if (response.status === 200) return true;
      else return false;
    })
    .catch((error) => {
      if (axios.isAxiosError(error)) return false;
      else throw error;
    });
};

export const register = async (
  registerData: RegisterData
): Promise<boolean> => {
  return axios
    .post('/api/auth/register', registerData)
    .then((response: AxiosResponse<boolean>) => {
      if (response.status === 200) return true;
      else return false;
    })
    .catch((error) => {
      if (axios.isAxiosError(error)) return false;
      else throw error;
    });
};

export const validateToken = async (): Promise<boolean> => {
  return axios
    .post('/api/auth/validate')
    .then((response: AxiosResponse<boolean>) => {
      if (response.status === 200) return true;
      else return false;
    })
    .catch(() => {
      return false;
    });
};

export const logout = async (): Promise<boolean> => {
  return axios
    .post('/api/auth/logout')
    .then((response: AxiosResponse<boolean>) => {
      if (response.status === 200) return true;
      else return false;
    })
    .catch((error) => {
      if (axios.isAxiosError(error) && error.status === 401) return true;
      else throw error;
    });
};

export const getRole = async (): Promise<string | null> => {
  return axios
    .get('/api/auth/role')
    .then((response: AxiosResponse<{ role: string }>) => {
      if (response.status === 200) return response.data?.role.toUpperCase();
      else return null;
    })
    .catch((error) => {
      if (axios.isAxiosError(error) && error.status === 401) return null;
      else throw error;
    });
};
