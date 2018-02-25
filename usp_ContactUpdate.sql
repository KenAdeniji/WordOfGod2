use WordEngineering;
go

drop procedure dbo.usp_ContactUpdate;
go

create procedure dbo.usp_ContactUpdate

	  @SequenceOrderId   int = null,
      @FirstName         varchar(255) = null,
	  @LastName          varchar(255) = null,
      @OtherName         varchar(255) = null,
      @Company    varchar(255)  = null

as



	update dbo.Contact

	set      FirstName = @FirstName,
		     LastName = @LastName,
             OtherName = @OtherName,
		     Company = @Company
    where    SequenceOrderId = @SequenceOrderId



go

grant execute on dbo.usp_ContactUpdate to public;
go

