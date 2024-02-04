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
import React from 'react';

interface LoginFormProps {
  setActiveTab: (value: React.SetStateAction<number>) => void;
}

export default function LoginForm({ setActiveTab }: LoginFormProps) {
  const handleClick = () => {
    setActiveTab(1);
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
          Вход в личный кабинет
        </Typography>
        <Box component='form' noValidate sx={{ mt: 1 }}>
          <TextField
            margin='normal'
            required
            fullWidth
            id='username'
            label='Имя пользователя или почта'
            name='username'
            autoComplete='email'
            autoFocus
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
            <a href='#' onClick={handleClick}>
              Регистрация
            </a>
          </p>
        </Box>
      </Box>
    </Container>
  );
}
