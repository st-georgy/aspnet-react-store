import { Container } from '@mui/material';
import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import logoSvg from '../../assets/logo.svg';
import { validateToken } from '../../utils/authApiUtils';
import LoginForm from '../LoginPage/LoginForm';
import Tabs from '../LoginPage/LoginPageTabs';
import RegisterForm from '../LoginPage/RegisterForm';
import ShowAlert from '../LoginPage/ShowAlert';

export default function LoginPage() {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const registerSuccess = queryParams.get('registerSuccess');

  const [value, setValue] = useState<number>(0);
  const [showRegisterAlert, setShowRegisterAlert] = useState<boolean>(false);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  useEffect(() => {
    validateToken().then((isTokenValid) => {
      if (isTokenValid) window.location.href = '/?loginSuccess=true';
    });

    if (registerSuccess === 'true') setShowRegisterAlert(true);
  }, []);

  return (
    <>
      <a href='/' style={{ position: 'absolute' }}>
        Вернуться на главную
      </a>
      <Container>
        <div style={{ textAlign: 'center' }}>
          <a href='/'>
            <img src={logoSvg} height='90' alt='Logo' />
          </a>
        </div>

        <Tabs value={value} onChange={handleChange} />

        {value === 0 && <LoginForm setActiveTab={setValue} />}
        {value === 1 && <RegisterForm setActiveTab={setValue} />}
      </Container>

      {showRegisterAlert && (
        <ShowAlert
          severity='success'
          message='Вы успешно зарегистрировались. Теперь вы можете войти.'
        />
      )}
    </>
  );
}
