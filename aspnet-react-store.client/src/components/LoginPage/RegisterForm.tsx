import { Box, Button, Container, TextField, Typography } from '@mui/material';
import React, { useState } from 'react';
import { register } from '../../utils/authApiUtils';
import ShowAlert from './ShowAlert';

interface RegisterFormProps {
  setActiveTab: (value: React.SetStateAction<number>) => void;
}

interface FormData {
  username: string;
  email: string;
  password: string;
}

export default function RegisterForm({ setActiveTab }: RegisterFormProps) {
  const [registerAlert, setRegisterAlert] = useState<boolean>(false);

  const [formData, setFormData] = useState<FormData>({
    username: '',
    email: '',
    password: '',
  });

  const [formErrors, setFormErrors] = useState<FormData>({
    username: '',
    email: '',
    password: '',
  });

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!validateForm()) return;

    register(formData).then((success) => {
      if (success) window.location.href = '/login?registerSuccess=true';
      else {
        setFormData({ username: '', email: '', password: '' });
        setFormErrors({ username: '', email: '', password: '' });
        setRegisterAlert(true);
      }
    });
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    setFormErrors({ ...formErrors, [name]: '' });
  };

  const handleBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (!value.trim()) {
      setFormErrors({
        ...formErrors,
        [name]: 'Поле обязательно для заполнения',
      });
    }
  };

  const validateForm = () => {
    const errors: FormData = {
      username: '',
      email: '',
      password: '',
    };

    if (!formData.username.trim())
      errors.password = 'Поле обязательно для заполнения';
    if (!formData.email.trim())
      errors.email = 'Поле обязательно для заполнения';
    if (!formData.password.trim())
      errors.password = 'Поле обязательно для заполнения';

    setFormErrors(errors);
    return !errors.username && !errors.email && !errors.password;
  };

  return (
    <Container component='main' maxWidth='xs'>
      {registerAlert && (
        <ShowAlert
          severity='error'
          message='Не удалось зарегистрироваться. Попробуйте еще раз.'
        />
      )}
      <Box
        sx={{
          mt: 2,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Typography component='h1' variant='h5'>
          Регистрация
        </Typography>
        <Box component='form' noValidate onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <TextField
            margin='normal'
            required
            fullWidth
            id='username'
            label='Имя пользователя'
            name='username'
            autoComplete='off'
            autoFocus
            value={formData.username}
            onChange={handleChange}
            onBlur={handleBlur}
            error={!!formErrors.username}
            helperText={formErrors.username}
          />
          <TextField
            margin='normal'
            required
            fullWidth
            id='email'
            label='Электронная почта'
            name='email'
            autoComplete='email'
            type='email'
            value={formData.email}
            onChange={handleChange}
            onBlur={handleBlur}
            error={!!formErrors.email}
            helperText={formErrors.email}
          />
          <TextField
            margin='normal'
            required
            fullWidth
            name='password'
            label='Пароль'
            type='password'
            id='password'
            value={formData.password}
            onChange={handleChange}
            onBlur={handleBlur}
            error={!!formErrors.password}
            helperText={formErrors.password}
          />
          <Button type='submit' fullWidth variant='contained' sx={{ mt: 3 }}>
            Регистрация
          </Button>
          <p style={{ textAlign: 'center' }}>
            Уже есть аккаунт?{' '}
            <a href='#' onClick={() => setActiveTab(0)}>
              Войти
            </a>
          </p>
        </Box>
      </Box>
    </Container>
  );
}
