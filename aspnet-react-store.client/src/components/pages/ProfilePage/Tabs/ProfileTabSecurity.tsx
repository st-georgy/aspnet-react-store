import { Button, Typography } from '@mui/material';
import { useState } from 'react';
import ShowAlert from '../../../shared/ShowAlert';
import ProfilePasswordDialog from '../ProfilePasswordDialog';
import TabPanel from '../ProfileTabPanel';

interface ProfileTabSecurityProps {
  index: number;
  tabValue: number;
}

export default function ProfileTabSecurity({
  index,
  tabValue,
}: ProfileTabSecurityProps) {
  const [passwordDialogOpen, setPasswordDialogOpen] = useState<boolean>(false);
  const [errorAlert, setErrorAlert] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>('');
  const [successAlert, setSuccessAlert] = useState<boolean>(false);

  const handleClickPassword = () => {
    setPasswordDialogOpen(true);
    setSuccessAlert(false);
    setErrorAlert(false);
  };

  const handlePasswordDialogClose = () => {
    setPasswordDialogOpen(false);
  };

  return (
    <>
      {errorAlert && <ShowAlert severity='error' message={errorMessage} />}
      {successAlert && (
        <ShowAlert severity='success' message='Пароль успешно обновлен' />
      )}
      <ProfilePasswordDialog
        open={passwordDialogOpen}
        onClose={handlePasswordDialogClose}
        setErrorAlert={setErrorAlert}
        setErrorMessage={setErrorMessage}
        setSuccessAlert={setSuccessAlert}
      />
      <TabPanel value={tabValue} index={index} header='Безопасность'>
        <Typography variant='h5' mt={5} mb={2} fontWeight='500'>
          Пароль
        </Typography>
        <div style={{ display: 'flex', marginTop: '2rem' }}>
          <Typography variant='h6' fontWeight='400'>
            Вы можете изменить текущий пароль
          </Typography>
          <Button
            variant='outlined'
            color='warning'
            sx={{ ml: 'auto' }}
            onClick={handleClickPassword}
          >
            Изменить пароль
          </Button>
        </div>
        <Typography variant='h5' mt={5} mb={2} fontWeight='500'>
          Деактивация аккаунта
        </Typography>
        <div style={{ display: 'flex', marginTop: '2rem' }}>
          <Typography variant='h6' fontWeight='400'>
            Вы можете удалить свой аккаунт
          </Typography>
          <Button variant='outlined' color='error' sx={{ ml: 'auto' }}>
            Удалить аккаунт
          </Button>
        </div>
      </TabPanel>
    </>
  );
}
