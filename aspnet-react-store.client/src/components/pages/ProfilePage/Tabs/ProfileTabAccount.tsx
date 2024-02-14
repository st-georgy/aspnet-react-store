import { Box, Button, Typography } from '@mui/material';
import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { IUserProfile } from '../../../../types/types';
import { updateProfile } from '../../../../utils/profileApiUtils';
import ShowAlert from '../../../shared/ShowAlert';
import ProfileField from '../ProfileField';
import TabPanel from '../ProfileTabPanel';

interface ProfileTabAccountProps {
  index: number;
  tabValue: number;
  profileData: IUserProfile;
  onProfileUpdate: (updatedProfile: IUserProfile) => void;
}

export default function ProfileTabAccount({
  index,
  tabValue,
  profileData,
  onProfileUpdate,
}: ProfileTabAccountProps) {
  const [editing, setEditing] = useState<boolean>(false);
  const [formData, setFormData] = useState<IUserProfile>(profileData);
  const [errorAlert, setErrorAlert] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>();
  const [successAlert, setSuccessAlert] = useState<boolean>(false);
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(false);

  const handleEdit = () => {
    setEditing(true);
    setSuccessAlert(false);
    setErrorAlert(false);
  };

  const handleCancel = () => {
    setEditing(false);
    setFormData(profileData);
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!formData.email?.trim() || !formData.userName?.trim()) {
      setErrorAlert(true);
      return;
    }

    const fetchData = async () => {
      try {
        const result = await updateProfile(formData);
        if (result) {
          onProfileUpdate(formData);
          setEditing(false);
          setSuccessAlert(true);
        }
      } catch (error) {
        let message = 'Не удалось сохранить профиль. ';
        if (axios.isAxiosError(error)) message += error.response?.data;
        setErrorMessage(message);
        setEditing(false);
        setFormData(profileData);
        setErrorAlert(true);
      }
    };

    fetchData();
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });

    if (!value.trim()) setSubmitDisabled(true);
    else setSubmitDisabled(false);
  };

  useEffect(() => {
    setFormData(profileData);
  }, [profileData]);

  if (!formData) return null;

  return (
    <TabPanel value={tabValue} index={index} header='Профиль'>
      {errorAlert && (
        <ShowAlert
          severity='error'
          message={errorMessage ?? 'Заполните все поля!'}
        />
      )}
      {successAlert && (
        <ShowAlert severity='success' message='Профиль успешно сохранён' />
      )}
      <Box component='form' noValidate onSubmit={handleSubmit}>
        <Typography variant='h5' mt={5} mb={2} fontWeight='500'>
          Аккаунт
        </Typography>
        <ProfileField
          title='Почта'
          value={formData.email}
          editing={editing}
          id='email'
          onChange={handleChange}
          isRequired
        />
        <ProfileField
          title='Имя пользователя'
          value={formData.userName}
          editing={editing}
          id='userName'
          onChange={handleChange}
          isRequired
        />
        <Typography variant='h5' mt={5} mb={2} fontWeight='500'>
          Имя
        </Typography>
        <ProfileField
          title='Фамилия'
          value={formData.lastName}
          editing={editing}
          id='lastName'
          onChange={handleChange}
        />
        <ProfileField
          title='Имя'
          value={formData.firstName}
          editing={editing}
          id='firstName'
          onChange={handleChange}
        />
        <ProfileField
          title='Отчество'
          value={formData.middleName}
          editing={editing}
          id='middleName'
          onChange={handleChange}
        />
        <div style={{ marginTop: '2rem', textAlign: 'right' }}>
          {!editing && (
            <Button variant='outlined' color='primary' onClick={handleEdit}>
              Редактировать
            </Button>
          )}
          {editing && (
            <>
              <Button variant='outlined' color='error' onClick={handleCancel}>
                Отменить
              </Button>
              <Button
                type='submit'
                variant='outlined'
                color='success'
                sx={{ ml: '1rem' }}
                disabled={submitDisabled}
              >
                Сохранить
              </Button>
            </>
          )}
        </div>
      </Box>
    </TabPanel>
  );
}
