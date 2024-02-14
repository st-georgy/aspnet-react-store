import { Typography } from '@mui/material';

interface TabPanelProps {
  header: string;
  children?: React.ReactNode;
  index: number;
  value: number;
}

export default function TabPanel(props: TabPanelProps) {
  const { header, children, value, index, ...other } = props;

  return (
    <div
      role='tabpanel'
      hidden={value !== index}
      id={`vertical-tabpanel-${index}`}
      aria-labelledby={`vertical-tab-${index}`}
      {...other}
    >
      {value === index && (
        <>
          <Typography variant='h4' fontWeight='600'>
            {header}
          </Typography>
          {children}
        </>
      )}
    </div>
  );
}
