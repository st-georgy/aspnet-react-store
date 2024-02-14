import { Box, Tab, Tabs } from '@mui/material';

interface AuthPageTabsProps {
  value: number;
  onChange: (_: React.SyntheticEvent, newValue: number) => void;
}

export default function AuthPageTabs({ value, onChange }: AuthPageTabsProps) {
  return (
    <Box sx={{ mt: 1 }}>
      <Tabs value={value} onChange={onChange} centered>
        <Tab label='Вход' />
        <Tab label='Регистрация' />
      </Tabs>
    </Box>
  );
}
