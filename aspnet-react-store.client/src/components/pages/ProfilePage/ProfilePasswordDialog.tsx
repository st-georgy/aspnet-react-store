import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField,
} from '@mui/material';
import axios from 'axios';
import React, { useState } from 'react';
import { updatePassword } from '../../../utils/profileApiUtils';

interface ProfilePasswordDialogProps {
  open: boolean;
  onClose: () => void;
  setErrorAlert: React.Dispatch<React.SetStateAction<boolean>>;
  setErrorMessage: React.Dispatch<React.SetStateAction<string>>;
  setSuccessAlert: React.Dispatch<React.SetStateAction<boolean>>;
}

interface FormData {
  oldPassword: string;
  newPassword: string;
}

export default function ProfilePasswordDialog({
  open,
  onClose,
  setErrorAlert,
  setErrorMessage,
  setSuccessAlert,
}: ProfilePasswordDialogProps) {
  const requiredMessage = 'Поле обязательно для заполнения!';
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true);

  const [formData, setFormData] = useState<FormData>({
    oldPassword: '',
    newPassword: '',
  });

  const [formErrors, setFormErrors] = useState<FormData>({
    oldPassword: '',
    newPassword: '',
  });

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!formData.oldPassword.trim() || !formData.newPassword.trim()) return;

    const fetchData = async () => {
      await updatePassword(formData.oldPassword, formData.newPassword)
        .then((success) => {
          if (success) setSuccessAlert(true);
        })
        .catch((error) => {
          let message = 'Не удалось обновить пароль. ';
          if (axios.isAxiosError(error)) message += error.response?.data;

          setErrorMessage(message);
          setErrorAlert(true);
        });
    };

    fetchData();
    setFormData({ oldPassword: '', newPassword: '' });
    setFormErrors({ oldPassword: '', newPassword: '' });
    onClose();
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    validate(name, value);
  };

  const validate = (name: string, value: string) => {
    if (!value.trim())
      setFormErrors({ ...formErrors, [name]: requiredMessage });
    else setFormErrors({ ...formErrors, [name]: '' });

    if (!formData.oldPassword.trim() || !formData.newPassword.trim())
      setSubmitDisabled(true);
    else setSubmitDisabled(false);
  };

  const handleBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    validate(name, value);
  };

  const handleClose = () => {
    setFormData({ oldPassword: '', newPassword: '' });
    setFormErrors({ oldPassword: '', newPassword: '' });
    onClose();
  };

  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        PaperProps={{
          component: 'form',
          onSubmit: handleSubmit,
          noValidate: true,
        }}
        fullWidth
      >
        <DialogTitle>Изменение пароля</DialogTitle>
        <DialogContent>
          <DialogContentText>Введите старый пароль:</DialogContentText>
          <TextField
            autoFocus
            required
            margin='dense'
            id='oldPassword'
            name='oldPassword'
            label='Старый пароль'
            placeholder='Старый пароль'
            type='password'
            fullWidth
            variant='standard'
            value={formData.oldPassword}
            onChange={handleChange}
            onBlur={handleBlur}
            error={!!formErrors.oldPassword}
            helperText={formErrors.oldPassword}
          />
          <DialogContentText mt={3}>Введите новый пароль:</DialogContentText>
          <TextField
            required
            margin='dense'
            id='newPassword'
            name='newPassword'
            label='Новый пароль'
            placeholder='Новый пароль'
            type='password'
            fullWidth
            variant='standard'
            value={formData.newPassword}
            onChange={handleChange}
            onBlur={handleBlur}
            error={!!formErrors.newPassword}
            helperText={formErrors.newPassword}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} color='error'>
            Отмена
          </Button>
          <Button type='submit' color='success' disabled={submitDisabled}>
            Изменить пароль
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}
