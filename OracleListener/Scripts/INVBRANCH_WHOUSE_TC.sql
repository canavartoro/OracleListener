
CREATE OR REPLACE TRIGGER INVBRANCH_WHOUSE_TC
AFTER INSERT ON UYUMSOFT.INVD_BRANCH_WHOUSE
FOR EACH ROW
DECLARE
  c  utl_tcp.connection;  -- TCP/IP connection to the Web server
  ret_val pls_integer; 
BEGIN

  c := utl_tcp.open_connection(remote_host => '127.0.0.1',
                               remote_port =>  4444,
                               charset     => 'US7ASCII');  -- open connection
  ret_val := utl_tcp.write_line(c, :NEW.WHOUSE_ID || '|DEPO');    -- send HTTP request
   UTL_TCP.flush (c);
  
  utl_tcp.close_connection(c);



END INVBRANCH_WHOUSE_TC;