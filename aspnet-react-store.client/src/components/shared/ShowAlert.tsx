import { Alert, AlertColor, Snackbar, SnackbarOrigin } from '@mui/material';
import { useState } from 'react';

interface ShowAlertProps {
  severity: AlertColor | undefined;
  message: string;
  autoHideDuration?: number;
  onClose?: () => void;
  open?: boolean;
  anchorOrigin?: SnackbarOrigin | undefined;
}

export default function ShowAlert({
  severity,
  message,
  autoHideDuration,
  onClose,
  open,
  anchorOrigin,
}: ShowAlertProps) {
  const [defaultOpen, setDefaultOpen] = useState(true);

  const handleClose = () => {
    setDefaultOpen(false);
  };

  return (
    <>
      <Snackbar
        open={open || defaultOpen}
        anchorOrigin={
          anchorOrigin || { vertical: 'bottom', horizontal: 'right' }
        }
        onClose={onClose || handleClose}
        autoHideDuration={autoHideDuration || 6000}
      >
        <Alert
          className='alert'
          severity={severity}
          onClose={onClose || handleClose}
        >
          {message}
        </Alert>
      </Snackbar>
    </>
  );
}
