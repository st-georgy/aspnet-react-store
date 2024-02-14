import { Container, Grid } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { IUser, IUserProfile } from '../../types/types';
import { getProfile } from '../../utils/profileApiUtils';
import NavBar from '../navigation/NavBar';
import TabPanel from './ProfilePage/ProfileTabPanel';
import ProfileTabs from './ProfilePage/ProfileTabs';
import ProfileTabAccount from './ProfilePage/Tabs/ProfileTabAccount';

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

  const onProfileUpdate = async (updatedProfile: IUserProfile) => {
    setProfileData(updatedProfile);
  };

  useEffect(() => {
    const fetchData = async () => {
      if (!currentUser) window.location.href = '/login';
      const profile: IUserProfile = await getProfile();
      setProfileData(profile);
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
            <ProfileTabAccount
              tabValue={tabValue}
              index={0}
              profileData={profileData!}
              onProfileUpdate={onProfileUpdate}
            />
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
