import { Box, Button, Container, TextField, Typography } from '@mui/material';
import React from 'react';

interface RegisterFormProps {
  setActiveTab: (value: React.SetStateAction<number>) => void;
}

export default function RegisterForm({ setActiveTab }: RegisterFormProps) {
  const handleClick = () => {
    setActiveTab(0);
  };

  return (
    <Container component='main' maxWidth='xs'>
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
        <Box component='form' noValidate sx={{ mt: 1 }}>
          <TextField
            margin='normal'
            required
            fullWidth
            id='username'
            label='Имя пользователя'
            name='username'
            autoComplete='off'
            autoFocus
          />
          <TextField
            margin='normal'
            required
            fullWidth
            id='email'
            label='Электронная почта'
            name='username'
            autoComplete='email'
            type='email'
          />
          <TextField
            margin='normal'
            required
            fullWidth
            name='password'
            label='Пароль'
            type='password'
            id='password'
          />
          <Button type='submit' fullWidth variant='contained' sx={{ mt: 3 }}>
            Регистрация
          </Button>
          <p style={{ textAlign: 'center' }}>
            Уже есть аккаунт?{' '}
            <a href='#' onClick={handleClick}>
              Войти
            </a>
          </p>
        </Box>
      </Box>
    </Container>
  );
}
