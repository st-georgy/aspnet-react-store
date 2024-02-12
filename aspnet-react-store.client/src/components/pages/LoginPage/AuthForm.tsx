import { Box, Button, Container, TextField, Typography } from '@mui/material';
import React, { useState } from 'react';
import { FormType } from '../../../types/types';
import ShowAlert from '../../shared/ShowAlert';

interface FormProps {
  setActiveTab: (value: React.SetStateAction<number>) => void;
  endpoint: (data: any) => Promise<boolean>;
  formType: FormType;
}

interface FormData {
  username?: string;
  email: string;
  password: string;
}

export default function AuthForm({
  setActiveTab,
  endpoint,
  formType,
}: FormProps) {
  const requiredMessage = 'Поле обязательно для заполнения';
  const alertMessage =
    formType === FormType.Login
      ? 'Не удалось авторизоваться. Попробуйте еще раз или восстановите пароль.'
      : 'Не удалось зарегистрироваться. Попробуйте еще раз.';

  const [alert, setAlert] = useState<boolean>(false);

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

    endpoint(formData).then((success) => {
      if (success && formType === FormType.Login)
        window.location.href = '/?loginSuccess=true';
      else if (success && formType === FormType.Register)
        window.location.href = '/login?registerSuccess=true';
      else {
        setFormData({ username: '', email: '', password: '' });
        setFormErrors({ username: '', email: '', password: '' });
        setAlert(true);
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
        [name]: requiredMessage,
      });
    }
  };

  const validateForm = () => {
    const errors: FormData = {
      username: '',
      email: '',
      password: '',
    };

    if (!formData.email.trim()) errors.email = requiredMessage;
    if (!formData.password.trim()) errors.password = requiredMessage;
    if (!formData.username?.trim() && formType === FormType.Register)
      errors.username = requiredMessage;

    setFormErrors(errors);
    return (
      !errors.email &&
      !errors.password &&
      (!errors.username || formType !== FormType.Register)
    );
  };

  return (
    <Container component='main' maxWidth='xs'>
      {alert && <ShowAlert severity='error' message={alertMessage} />}
      <Box
        sx={{
          mt: 2,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Typography component='h1' variant='h5'>
          {formType === FormType.Login
            ? 'Вход в личный кабинет'
            : 'Регистрация'}
        </Typography>
        <Box component='form' noValidate onSubmit={handleSubmit} sx={{ mt: 1 }}>
          {formType === FormType.Register && (
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
          )}
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
            {formType === FormType.Login ? 'Войти' : 'Регистрация'}
          </Button>
          <p style={{ textAlign: 'center' }}>
            {formType === FormType.Login
              ? 'Нет аккаунта? '
              : 'Уже есть аккаунт? '}
            <a
              href='#'
              onClick={() => setActiveTab(formType === FormType.Login ? 1 : 0)}
            >
              {formType === FormType.Login ? 'Регистрация' : 'Вход'}
            </a>
          </p>
        </Box>
      </Box>
    </Container>
  );
}
