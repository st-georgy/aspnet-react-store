import {
  Box,
  Button,
  Checkbox,
  Container,
  FormControlLabel,
  Grid,
  Link,
  TextField,
  Typography,
} from '@mui/material';
import React, { useState } from 'react';
import { login } from '../../utils/authApiUtils';
import ShowAlert from './ShowAlert';

interface LoginFormProps {
  setActiveTab: (value: React.SetStateAction<number>) => void;
}

interface FormData {
  email: string;
  password: string;
}

interface FormErrors {
  email: string;
  password: string;
}

export default function LoginForm({ setActiveTab }: LoginFormProps) {
  const [loginAlert, setLoginAlert] = useState<boolean>(false);

  const [formData, setFormData] = useState<FormData>({
    email: '',
    password: '',
  });

  const [formErrors, setFormErrors] = useState<FormErrors>({
    email: '',
    password: '',
  });

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!validateForm()) return;

    login(formData).then((success) => {
      if (success) window.location.href = '/?loginSuccess=true';
      else {
        setFormData({ email: '', password: '' });
        setFormErrors({ email: '', password: '' });
        setLoginAlert(true);
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
    const errors: FormErrors = {
      email: '',
      password: '',
    };

    if (!formData.email.trim())
      errors.email = 'Поле обязательно для заполнения';
    if (!formData.password.trim())
      errors.password = 'Поле обязательно для заполнения';

    setFormErrors(errors);
    return !errors.email && !errors.password;
  };

  return (
    <Container component='main' maxWidth='xs'>
      {loginAlert && (
        <ShowAlert
          severity='error'
          message='Не удалось авторизоваться. Попробуйте еще раз или восстановите пароль.'
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
          Вход в личный кабинет
        </Typography>
        <Box component='form' noValidate onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <TextField
            margin='normal'
            required
            fullWidth
            id='email'
            label='Почта'
            name='email'
            autoComplete='email'
            autoFocus
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
          <Grid container>
            <Grid item xs>
              <FormControlLabel
                control={
                  <Checkbox value='remember' color='primary' id='rememberme' />
                }
                label='Запомнить меня'
              />
            </Grid>
            <Grid item sx={{ mt: 1 }}>
              <Link href='#' variant='body2'>
                Забыли пароль?
              </Link>
            </Grid>
          </Grid>
          <Button type='submit' fullWidth variant='contained' sx={{ mt: 3 }}>
            Войти
          </Button>
          <p style={{ textAlign: 'center' }}>
            Нет аккаунта?{' '}
            <a href='#' onClick={() => setActiveTab(1)}>
              Регистрация
            </a>
          </p>
        </Box>
      </Box>
    </Container>
  );
}
