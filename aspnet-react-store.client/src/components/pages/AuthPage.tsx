import { Container, Link } from '@mui/material';
import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import logoSvg from '../../assets/logo.svg';
import { FormType } from '../../types/types';
import { login, register, validateToken } from '../../utils/authApiUtils';
import ShowAlert from '../shared/ShowAlert';
import AuthForm from './AuthPage/AuthForm';
import Tabs from './AuthPage/AuthPageTabs';

export default function AuthPage() {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const registerSuccess = queryParams.get('registerSuccess');

  const [activeTab, setActiveTab] = useState<number>(0);
  const [showRegisterAlert, setShowRegisterAlert] = useState<boolean>(false);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setActiveTab(newValue);
  };

  useEffect(() => {
    const handleTokenValidation = async () => {
      const isTokenValid = await validateToken();
      if (isTokenValid) window.location.href = '/?loginSuccess=true';
    };

    const handleRegisterSuccess = () => {
      if (registerSuccess === 'true') setShowRegisterAlert(true);
    };

    handleTokenValidation();
    handleRegisterSuccess();
  }, []);

  return (
    <>
      <Link href='/' style={{ position: 'absolute' }}>
        Вернуться на главную
      </Link>
      <Container>
        <div style={{ textAlign: 'center' }}>
          <Link href='/'>
            <img src={logoSvg} height='90' alt='Logo' />
          </Link>
        </div>

        <Tabs value={activeTab} onChange={handleChange} />

        {activeTab === 0 && (
          <AuthForm
            setActiveTab={setActiveTab}
            endpoint={login}
            formType={FormType.Login}
          />
        )}
        {activeTab === 1 && (
          <AuthForm
            setActiveTab={setActiveTab}
            endpoint={register}
            formType={FormType.Register}
          />
        )}
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
