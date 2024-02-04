import { Box, Tab, Tabs } from '@mui/material';

interface LoginPageTabsProps {
  value: number;
  onChange: (_: React.SyntheticEvent, newValue: number) => void;
}

export default function LoginPageTabs({ value, onChange }: LoginPageTabsProps) {
  return (
    <Box sx={{ mt: 1 }}>
      <Tabs value={value} onChange={onChange} centered>
        <Tab label='Вход' />
        <Tab label='Регистрация' />
      </Tabs>
    </Box>
  );
}
