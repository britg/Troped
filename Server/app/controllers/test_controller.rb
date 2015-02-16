class TestController < WebsocketRails::BaseController

  def test
    puts "got a request #{message}"
    trigger_success "hello"
  end

end