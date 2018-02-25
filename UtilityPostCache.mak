all: PostCache.aspx.cs.dll

PostCache.aspx.cs.dll: PostCache.aspx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\PostCacheDocumentation.xml /out:bin\PostCache.aspx.cs.dll /target:library /unsafe PostCache.aspx.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation