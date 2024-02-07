import axios, { AxiosResponse } from 'axios';

interface LoginData {
  email: string;
  password: string;
}

export const login = async (loginData: LoginData): Promise<boolean> => {
  return axios
    .post('/api/users/login', loginData)
    .then((response: AxiosResponse<boolean>) => {
      if (response.status === 200) return true;
      else return false;
    })
    .catch((error) => {
      if (axios.isAxiosError(error)) return false;
      throw error;
    });
};

export const validateToken = async (): Promise<boolean> => {
  return axios
    .post('/api/users/validate')
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
    .post('/api/users/logout')
    .then((response: AxiosResponse<boolean>) => {
      if (response.status === 200) return true;
      else return false;
    })
    .catch((error) => {
      if (axios.isAxiosError(error) && error.status === 401) return true;
      throw error;
    });
};
