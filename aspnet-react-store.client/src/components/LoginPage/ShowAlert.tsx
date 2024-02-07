import { Alert, AlertColor, Snackbar } from '@mui/material';
import { useState } from 'react';

interface ShowAlertProps {
  severity: AlertColor | undefined;
  message: string;
}

export default function ShowAlert({ severity, message }: ShowAlertProps) {
  const [open, setOpen] = useState(true);

  return (
    <>
      <Snackbar
        open={open}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        onClose={() => {
          setOpen(false);
        }}
        autoHideDuration={6000}
      >
        <Alert
          className='alert'
          severity={severity}
          onClose={() => {
            setOpen(false);
          }}
        >
          {message}
        </Alert>
      </Snackbar>
    </>
  );
}
