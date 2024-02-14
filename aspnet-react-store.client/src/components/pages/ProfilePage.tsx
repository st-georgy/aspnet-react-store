import { Container, Grid, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { IUser, IUserProfile } from '../../types/types';
import { getProfile } from '../../utils/profileApiUtils';
import NavBar from '../navigation/NavBar';
import ProfileField from './ProfilePage/ProfileField';
import TabPanel from './ProfilePage/ProfileTabPanel';
import ProfileTabs from './ProfilePage/ProfileTabs';

interface ProfilePageProps {
  currentUser: IUser | null;
  isAdmin: boolean;
}

export default function ProfilePage({
  currentUser,
  isAdmin,
}: ProfilePageProps) {
  const [profileData, setProfileData] = useState<IUserProfile>();
  const [tabValue, setTabValue] = useState<number>(0);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setTabValue(newValue);
  };

  useEffect(() => {
    const fetchData = async () => {
      if (!currentUser) window.location.href = '/login';
      setProfileData(await getProfile());
    };

    fetchData();
  }, []);

  return (
    <>
      <NavBar currentUser={currentUser} isAdmin={isAdmin} showSearch={false} />
      <Container sx={{ mt: '6rem' }}>
        <Grid container spacing={4}>
          <Grid item xs={6} md={3}>
            <ProfileTabs tabValue={tabValue} onChange={handleChange} />
          </Grid>
          <Grid item xs={6} md={9} className='options-grid'>
            <TabPanel value={tabValue} index={0} header='Профиль'>
              <Typography variant='h5' mt={5} mb={2} fontWeight='500'>
                Аккаунт
              </Typography>
              <ProfileField title='Почта' value={profileData?.email} />
              <ProfileField
                title='Имя пользователя'
                value={profileData?.userName}
              />
              <ProfileField title='Пароль' value='********' />
              <Typography variant='h5' mt={5} mb={2} fontWeight='500'>
                Имя
              </Typography>
              <ProfileField title='Фамилия' value={profileData?.lastName} />
              <ProfileField title='Имя' value={profileData?.firstName} />
              <ProfileField title='Отчество' value={profileData?.middleName} />
            </TabPanel>

            <TabPanel value={tabValue} index={1} header='Заказы'></TabPanel>
            <TabPanel value={tabValue} index={2} header='Платежи'></TabPanel>
            <TabPanel
              value={tabValue}
              index={3}
              header='Деактивация'
            ></TabPanel>
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
