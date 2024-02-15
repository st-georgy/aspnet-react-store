import { Tab, Tabs } from '@mui/material';

interface ProfileTabsProps {
  tabValue: number;
  onChange: (_: React.SyntheticEvent, newValue: number) => void;
}

export default function ProfileTabs({ tabValue, onChange }: ProfileTabsProps) {
  const tabStyle = (index: number) => ({
    textTransform: 'none',
    textAlign: 'left',
    alignItems: 'flex-start',
    minHeight: 'unset',
    height: '36px',
    marginTop: '5px',
    color: '#000',
    backgroundColor: index === tabValue ? '#F1F0F5' : 'transparent',
    borderRadius: '5px',
    '&:hover': {
      backgroundColor: index === tabValue ? '#E1E1E5' : '#F1F0F5',
    },
  });

  function a11yProps(index: number) {
    return {
      id: `vertical-tab-${index}`,
      'aria-controls': `vertical-tabpanel-${index}`,
    };
  }
  return (
    <Tabs
      orientation='vertical'
      value={tabValue}
      onChange={onChange}
      TabIndicatorProps={{
        style: { display: 'none' },
      }}
      textColor='inherit'
    >
      <Tab label='Профиль' {...a11yProps(0)} sx={tabStyle(0)}></Tab>
      <Tab label='Заказы' {...a11yProps(2)} sx={tabStyle(1)}></Tab>
      <Tab label='Платежи' {...a11yProps(3)} sx={tabStyle(2)}></Tab>
      <Tab label='Безопасность' {...a11yProps(4)} sx={tabStyle(3)}></Tab>
    </Tabs>
  );
}
