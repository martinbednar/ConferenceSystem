DECLARE @cnt INT = 5;

WHILE @cnt < 251
BEGIN

	INSERT INTO dbo.AspNetUsers (
		RegisterTimestamp,
		FirstName,
		LastName,
		Agreement,
		VariableSymbol,
		IsAlternate,
		Email,
		PasswordHash,
		SecurityStamp,
		WantCert,
		WasEmailWorkshopSent,
		WasEmailCarteringSent,
		EmailConfirmed,
		PhoneNumberConfirmed,
		TwoFactorEnabled,
		LockoutEnabled,
		AccessFailedCount,
		UserName
	) VALUES (
		'2018-12-16 11:40:46.000',
		'Test',
		'Test',
		'false',
		0,
		'false',
		'martinbedn20@seznam.cz',
		'APqSb/sk/WCLZHnnE1KFPhNZmcFw32/lf/8a2wgkC6NXhiOFyy2CVs4hFzaJ3hCBUA==',
		'5905b1a5-c04a-4a8c-9cae-19dfb4f8625b',
		'false',
		'false',
		'false',
		'false',
		'false',
		'false',
		'false',
		0,
		'martinbedn20@seznam.cz'+CONVERT(varchar(4), @cnt)
	);
	SET @cnt = @cnt + 1;
END;