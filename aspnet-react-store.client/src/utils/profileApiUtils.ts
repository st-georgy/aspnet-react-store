import axios from 'axios';
import { IUserProfile } from '../types/types';

interface ProfileData {
  username?: string | null;
  email?: string | null;
  fistname?: string | null;
  middlename?: string | null;
  lastname?: string | null;
}

const handleError = (error: any): any => {
  if (axios.isAxiosError(error) && error.response?.status === 401)
    window.location.href = '/login';
  throw error;
};

export const getProfile = async (): Promise<IUserProfile> => {
  try {
    const response = await axios.get<IUserProfile>('/api/profile');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updateProfile = async (
  profileData: ProfileData
): Promise<boolean> => {
  try {
    await axios.put('/api/profile', profileData, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return true;
  } catch (error) {
    return handleError(error);
  }
};

export const updatePassword = async (
  oldPassword: string,
  newPassword: string
): Promise<boolean> => {
  try {
    await axios.put(
      '/api/profile/password',
      { oldPassword, newPassword },
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
    return true;
  } catch (error) {
    return handleError(error);
  }
};
