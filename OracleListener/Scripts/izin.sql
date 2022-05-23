--sample, şifre:edirsel
DECLARE
  c  utl_tcp.connection;  -- TCP/IP connection to the Web server
  ret_val pls_integer; 
BEGIN
  c := utl_tcp.open_connection(remote_host => '127.0.0.1',
                               remote_port =>  4444,
                               charset     => 'US7ASCII');  -- open connection
  ret_val := utl_tcp.write_line(c, '3151|DEPO');    -- send HTTP request
   UTL_TCP.flush (c);
  
  utl_tcp.close_connection(c);
END;



CREATE OR REPLACE TRIGGER UYUMSOFT.INVBRANCH_WHOUSE_TC
AFTER INSERT OR UPDATE ON UYUMSOFT.INVD_BRANCH_WHOUSE
FOR EACH ROW
DECLARE
  c  utl_tcp.connection;  -- TCP/IP connection to the Web server
  ret_val pls_integer; 
BEGIN

  c := utl_tcp.open_connection(remote_host => '10.225.0.202',
                               remote_port =>  4444,
                               charset     => 'US7ASCII');  -- open connection
  ret_val := utl_tcp.write_line(c, :NEW.WHOUSE_ID || '|DEPO');    -- send HTTP request
   UTL_TCP.flush (c);
  
  utl_tcp.close_connection(c);



END INVBRANCH_WHOUSE_TC;
/



grant  execute on UTL_TCP to uyumsoft;

grant  execute on UTL_SMTP to uyumsoft;

grant  execute on UTL_MAIL to uyumsoft;

grant  execute on UTL_HTTP to uyumsoft;

grant  execute on UTL_INADDR to uyumsoft;